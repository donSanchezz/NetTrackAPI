using NetTrackAPI.ViewModels;

namespace NetTrackAPI.Repositories.Alert
{
    public interface IAlertRepository 
    {
        Task Start(HttpRequest request);
        Task SaveImage(HttpRequest request);
    }
}
