using System.Net;
using System.Net.Security;
using AutoInterfaceAttributes;
using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace Shovel.Services;

[AutoInterface]
public sealed class ProxyService : IProxyService, IDisposable
{
    private static readonly string[] RedirectDomains =
    [
        ".yuanshen.com",
        ".hoyoverse.com",
        ".mihoyo.com",
        ".yuanshen.com:12401",
        ".zenlesszonezero.com",
        ".honkaiimpact3.com",
        ".bhsr.com",
        ".starrails.com",
        ".kurogame.net",
        ".kurogame-service.com",
        ".kurogame.com",
        ".g3.proletariat.com",
        ".honkaiimpact3.com",
        ".bh3.com"
    ];

    private readonly ProxyServer _server;
    private readonly ExplicitProxyEndPoint _proxyEndPoint;

    public ProxyService()
    {
        _server = new ProxyServer();
        _proxyEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8080);

        _server.CertificateManager.EnsureRootCertificate();
        _server.BeforeRequest += BeforeRequest;
        _server.ServerCertificateValidationCallback += OnCertValidation;

        _proxyEndPoint.BeforeTunnelConnectRequest += BeforeTunnelConnectRequest;
    }

    public string Address { get; set; } = "127.0.0.1";

    public int Port { get; set; } = 8888;

    public void Start()
    {
        if (_server.ProxyRunning)
            return;

        _server.AddEndPoint(_proxyEndPoint);
        _server.Start();

        _server.SetAsSystemHttpProxy(_proxyEndPoint);
        _server.SetAsSystemHttpsProxy(_proxyEndPoint);
    }

    public void Stop()
    {
        if (_server.ProxyRunning)
        {
            _server.Stop();
        }
    }

    public void Shutdown()
    {
        _server.Stop();
        _server.Dispose();
    }

    private Task BeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs args)
    {
        string hostname = args.HttpClient.Request.RequestUri.Host;
        args.DecryptSsl = ShouldRedirect(hostname);

        return Task.CompletedTask;
    }

    private Task OnCertValidation(object sender, CertificateValidationEventArgs args)
    {
        if (args.SslPolicyErrors == SslPolicyErrors.None)
        {
            args.IsValid = true;
        }

        return Task.CompletedTask;
    }

    private Task BeforeRequest(object sender, SessionEventArgs args)
    {
        string hostname = args.HttpClient.Request.RequestUri.Host;

        if (ShouldRedirect(hostname))
        {
            string requestUrl = args.HttpClient.Request.Url;

            Uri local = new($"http://{Address}:{Port}/");

            string replacedUrl = new UriBuilder(requestUrl)
            {
                Scheme = local.Scheme,
                Host = local.Host,
                Port = local.Port
            }.Uri.ToString();

            args.HttpClient.Request.Url = replacedUrl;

            Console.WriteLine(requestUrl);
        }

        return Task.CompletedTask;
    }

    private static bool ShouldRedirect(string hostname) => RedirectDomains.Any(hostname.EndsWith);

    public void Dispose()
    {
        Shutdown();
    }
}
