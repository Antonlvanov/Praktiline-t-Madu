using NAudio.Wave;
using System;
using System.IO;

namespace Praktiline_töö_Madu
{
    public class Sounds
    {
        private string pathToMedia;
        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        private bool isLooping; // Флаг для зацикливания

        public Sounds()
        {
            var ind = Directory.GetCurrentDirectory().ToString().IndexOf("bin", StringComparison.Ordinal);
            string binFolder = Directory.GetCurrentDirectory().Substring(0, ind).ToString();
            pathToMedia = binFolder + "resources\\";
        }

        public void Play(string songName)
        {
            PlaySound(songName + ".mp3", loop: false, volume: 0.3f);
        }

        public void PlayEat()
        {
            PlaySound("eat.mp3", loop: false, volume: 0.4f);
        }

        public void PlayEatPoison()
        {
            PlaySound("cough.mp3", loop: false, volume: 0.5f);
        }

        public void SpeedUp()
        {
            PlaySound("boost.mp3", loop: false, volume: 0.4f);
        }
        
        public void SlowDown()
        {
            PlaySound("power-down.mp3", loop: false, volume: 2.5f);
        }

        public void GameOver()
        {
            PlaySound("game-over.mp3", loop: false, volume: 1f);
        }

        public void PlayTheme()
        {
            PlaySound("8bit-music-for-game.mp3", loop: true, volume: 0.3f);
        }

        private void PlaySound(string fileName, bool loop, float volume)
        {
            string fullPath = pathToMedia + fileName;
            isLooping = loop;

            // volume
            audioFileReader = new AudioFileReader(fullPath)
            {
                Volume = volume
            };

            waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);
            waveOut.Play();

            waveOut.PlaybackStopped += OnPlaybackStopped;
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            if (isLooping && waveOut != null && audioFileReader != null)
            {
                audioFileReader.Position = 0;
                waveOut.Play();
            }
        }

        public void Stop()
        {
            waveOut.PlaybackStopped -= OnPlaybackStopped;
            waveOut.Stop();
            waveOut.Dispose();
            waveOut = null;
            audioFileReader.Dispose();
            audioFileReader = null;
        }
    }
}
