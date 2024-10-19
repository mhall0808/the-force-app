// src/components/StarWarsCrawl.tsx

import React, { useEffect, useRef, useState } from 'react';
import './StarWarsCrawl.css';

interface StarWarsCrawlProps {
  text: string[];
  onSkip: () => void;
  duration?: number; // Duration in seconds
}

const StarWarsCrawl: React.FC<StarWarsCrawlProps> = ({ text, onSkip, duration = 40 }) => {
  const audioRef = useRef<HTMLAudioElement | null>(null);
  const [isAnimationStarted, setIsAnimationStarted] = useState(false);

  useEffect(() => {
    // Start the animation when the text is loaded
    if (text.length > 0 && !isAnimationStarted) {
      setIsAnimationStarted(true);

      // Start the audio
      if (audioRef.current) {
        audioRef.current.play().catch((error) => {
          console.error('Error playing audio:', error);
        });
      }
    }
  }, [text, isAnimationStarted]);

  useEffect(() => {
    return () => {
      // Stop the audio when the component unmounts
      if (audioRef.current) {
        audioRef.current.pause();
        audioRef.current.currentTime = 0;
      }
    };
  }, []);

  return (
    <div className="star-wars-crawl">
      <div
        className={`crawl-content ${isAnimationStarted ? 'animate' : ''}`}
        style={{ '--animation-duration': `${duration}s` } as React.CSSProperties}
      >
        {text.map((paragraph, index) => (
          <p key={index}>{paragraph}</p>
        ))}
      </div>
      <button className="skip-button" onClick={onSkip}>Skip</button>
      {/* Audio Element */}
      <audio ref={audioRef} src="/assets/star-wars-theme.mp3" />
    </div>
  );
};

export default StarWarsCrawl;
