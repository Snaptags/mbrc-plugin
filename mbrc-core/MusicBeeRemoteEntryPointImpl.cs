using System.Net;
using WebSocketProxy;

namespace MusicBeeRemoteCore
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    using MusicBeeRemoteCore.AndroidRemote;
    using MusicBeeRemoteCore.AndroidRemote.Controller;
    using MusicBeeRemoteCore.AndroidRemote.Entities;
    using MusicBeeRemoteCore.AndroidRemote.Events;
    using MusicBeeRemoteCore.AndroidRemote.Model;
    using MusicBeeRemoteCore.AndroidRemote.Networking;
    using MusicBeeRemoteCore.AndroidRemote.Persistence;
    using MusicBeeRemoteCore.AndroidRemote.Utilities;
    using MusicBeeRemoteCore.Interfaces;
    using MusicBeeRemoteCore.Modules;

    using Nancy.Hosting.Self;

    using Ninject;

    using NLog;
    using NLog.Config;
    using NLog.Targets;

    public class MusicBeeRemoteEntryPointImpl : IMusicBeeRemoteEntryPoint
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Subject<string> eventDebouncer = new Subject<string>();

        private StandardKernel kernel;

        private IMessageHandler messageHandler;

        public int CachedCoverCount
        {
            get
            {
                var module = this.kernel.Get<LibraryModule>();
                return module.GetCachedCoverCount();
            }
        }

        public int CachedTrackCount
        {
            get
            {
                var module = this.kernel.Get<LibraryModule>();
                return module.GetCachedTrackCount();
            }
        }

        public PersistenceController Settings { get; private set; }

        public string StoragePath { get; set; }

        public void CacheCover(string cover)
        {
            var model = this.kernel.Get<LyricCoverModel>();
            model.SetCover(cover);
        }

        public void CacheLyrics(string lyrics)
        {
            if (string.IsNullOrEmpty(lyrics))
            {
                lyrics = "Lyrics Not Found";
            }

            var model = this.kernel.Get<LyricCoverModel>();
            model.Lyrics = lyrics;
        }

        public EventBus GetBus()
        {
            return this.kernel.Get<EventBus>();
        }

        public IKernel GetKernel()
        {
            return this.kernel;
        }

        public void Init(IBindingProvider provider)
        {
            InitializeLoggingConfiguration(this.StoragePath);
            Debug.WriteLine("MusicBee Remote initializing");
            Logger.Debug("MusicBee Remote initializing");

            InjectionModule.StoragePath = this.StoragePath;

            Utilities.StoragePath = this.StoragePath;

            this.kernel = new StandardKernel(new InjectionModule(provider));

            this.Settings = this.kernel.Get<PersistenceController>();
            this.Settings.LoadSettings();

            var controller = this.kernel.Get<Controller>();
            Configuration.Register(controller);
            controller.InjectKernel(this.kernel);
                    
            var libraryModule = this.kernel.Get<LibraryModule>();
            var playlistModule = this.kernel.Get<IPlaylistModule>();

            var bus = this.kernel.Get<EventBus>();

            bus.Publish(new MessageEvent(MessageEvent.ActionSocketStart));
            bus.Publish(new MessageEvent(MessageEvent.StartServiceBroadcast));
            bus.Publish(new MessageEvent(MessageEvent.ShowFirstRunDialog));

            this.BuildCache(libraryModule, playlistModule);

            this.StartHttp();
            this.StartProxy();

            this.eventDebouncer.Throttle(TimeSpan.FromSeconds(1)).Subscribe(eventType => this.Notify(eventType, false));
        }

        public void Notify(string eventType, bool debounce)
        {
            if (debounce)
            {
                this.eventDebouncer.OnNext(eventType);
                return;
            }

            var server = this.kernel.Get<SocketServer>();
            var notification = new NotificationMessage { Message = eventType };

            server.Send(notification.ToJsonString());
        }

        public void SetMessageHandler(IMessageHandler messageHandler)
        {
            this.messageHandler = messageHandler;
        }

        public void SetVersion(string version)
        {
            this.Settings.Settings.CurrentVersion = version;
        }

        public void FileAdded(string sourceUrl)
        {
            Logger.Debug($"new file added {sourceUrl}");
        }

        public void FileDeleted(string sourceUrl)
        {
            Logger.Debug($"new file deleted {sourceUrl}");
        }

        public void TagsChanged(string sourceUrl)
        {
            Logger.Debug($"tags changed {sourceUrl}");
        }

        private void BuildCache(LibraryModule libraryModule, IPlaylistModule playlistModule)
        {
            var observable = Observable.Create<string>(
                o =>
                    {
                        o.OnNext(@"MBRC: building library cache.");
                        libraryModule.BuildCache();
                        o.OnNext(@"MBRC: Synchronizing playlists.");
                        playlistModule.SyncPlaylistsWithCache();
                        o.OnNext(@"MBRC: Processing album covers.");
                        libraryModule.BuildCoverCachePerAlbum();
                        o.OnNext(@"MBRC: Cache Ready.");

                        o.OnCompleted();
                        return () => { };
                    });

            observable.SubscribeOn(ThreadPoolScheduler.Instance)
            .ObserveOn(ThreadPoolScheduler.Instance)
                .Subscribe(
                    s =>
                    {
                        this.messageHandler?.OnMessageAvailable(s);
                        Logger.Debug(s);
                    },
                    ex => { Logger.Debug(ex, "Library sync failed"); });
        }

        /// <summary>
        ///     Initializes the logging configuration.
        /// </summary>
        /// <param name="storagePath"></param>
        public static void InitializeLoggingConfiguration(string storagePath)
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            var fileTarget = new FileTarget();
            var debugger = new DebuggerTarget();

#if DEBUG
            var sentinalTarget = new NLogViewerTarget()
            {
                Name = "sentinel",
                Address = "udp://127.0.0.1:9999",
                IncludeNLogData = true,
                IncludeSourceInfo = true
            };

            var sentinelRule = new LoggingRule("*", LogLevel.Trace, sentinalTarget);
            config.AddTarget("sentinel", sentinalTarget);
            config.LoggingRules.Add(sentinelRule);
#endif

            config.AddTarget("console", consoleTarget);
            config.AddTarget("file", fileTarget);
            config.AddTarget("debugger", debugger);

            consoleTarget.Layout = @"${date:format=HH\\:MM\\:ss} ${logger} ${message} ${exception}";
            fileTarget.FileName = $"{storagePath}\\error.log";
            fileTarget.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}||${exception}";

            debugger.Layout = fileTarget.Layout;

            var consoleRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(consoleRule);

#if DEBUG
            var fileRule = new LoggingRule("*", LogLevel.Debug, fileTarget);
#else
            var fileRule = new LoggingRule("*", LogLevel.Error, fileTarget);
#endif
            config.LoggingRules.Add(fileRule);

            var debuggerRule = new LoggingRule("*", LogLevel.Debug, debugger);
            config.LoggingRules.Add(debuggerRule);

            LogManager.Configuration = config;
        }

        private void StartHttp()
        {
            try
            {
                var bootstrapper = new Bootstrapper(this.kernel);
                var configuration = new HostConfiguration
                {
                    RewriteLocalhost = true,
                    UrlReservations = {CreateAutomatically = true}
                };
                var nancyHost = new NancyHost(
                    new Uri($"http://localhost:{this.Settings.Settings.HttpPort}/"), 
                    bootstrapper, configuration);
                
                nancyHost.Start();
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
            }
        }


        private void StartProxy()
        {
            var configuration = new TcpProxyConfiguration
            {
                HttpHost = new Host
                {
                    Port = (int) this.Settings.Settings.HttpPort,
                    IpAddress = IPAddress.Loopback
                },
                PublicHost = new Host
                {
                    Port = (int) this.Settings.Settings.ProxyPort,
                    IpAddress = IPAddress.Parse("0.0.0.0")
                },
                WebSocketHost = new Host
                {
                    Port = (int)this.Settings.Settings.WebSocketPort,
                    IpAddress = IPAddress.Loopback
                }
            };
            
            var tcpProxy = new TcpProxyServer(configuration);
            tcpProxy.Start();
        }
    }
}