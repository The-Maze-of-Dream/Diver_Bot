namespace Diver.DAL.Interfaces;

public interface IMusicRepository
{
    Task PlayAsync (string trackUrl);
    Task StopAsync();
    Task PauseAsync();
    Task ResumeAsync();
    Task SkipAsync();
    Task GetQueuedTracksAsync();
    Task Disconnect();
    Task ClearQueuedTracksAsync();
}