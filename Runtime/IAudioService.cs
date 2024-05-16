using Kuroneko.UtilityDelivery;

namespace Kuroneko.AudioDelivery
{
    public interface IAudioService : IGameService
    {
        public void Play(string clipName);
        public void Stop(string clipName);
    }
}
