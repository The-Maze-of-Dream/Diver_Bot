using Diver.DAL.Interfaces;
using Victoria;

namespace Diver.DAL.Repositories;

public class MusicRepository: IMusicRepository
{
    private readonly LavaNode _lavaNode;

    public MusicRepository(LavaNode lavaNode)
    {
        _lavaNode = lavaNode;
    }

    public async Task PlayAsync(string trackUrl)
    {
        
    }

    public Task StopAsync()
    {
        throw new NotImplementedException();
    }

    public Task PauseAsync()
    {
        throw new NotImplementedException();
    }

    public Task ResumeAsync()
    {
        throw new NotImplementedException();
    }

    public Task SkipAsync()
    {
        throw new NotImplementedException();
    }

    public Task GetQueuedTracksAsync()
    {
        throw new NotImplementedException();
    }

    public Task Disconnect()
    {
        throw new NotImplementedException();
    }

    public Task ClearQueuedTracksAsync()
    {
        throw new NotImplementedException();
    }
}