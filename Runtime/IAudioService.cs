using Kuroneko.UtilityDelivery;

namespace Kuroneko.AudioDelivery
{
    public interface IAudioService : IGameService
    {
        public void Play(string clipName, string instanceId = "");
        public void Pause(string clipName, string instanceId = "");
        public void Resume(string clipName, string instanceId = "");
        public void Stop(string clipName, string instanceId = "");
    }
}
