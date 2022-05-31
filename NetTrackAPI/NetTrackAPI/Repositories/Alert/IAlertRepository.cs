using NetTrackAPI.ViewModels;

namespace NetTrackAPI.Repositories.Alert
{
    public interface IAlertRepository 
    {
        Task Start(HttpRequest request);
        Task SaveImage(HttpRequest request);
        Task<Models.Alert> GetAlert(string userId);
        Task UpdateAlert(HttpRequest request);

        Task StopAlert(HttpRequest request);
    }
}
