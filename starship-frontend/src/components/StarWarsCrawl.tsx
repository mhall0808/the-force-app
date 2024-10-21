// src/components/StarWarsCrawl.tsx

import React, { useEffect, useRef } from 'react';
import './StarWarsCrawl.css';

interface StarWarsCrawlProps {
  text: string[];
  onSkip: () => void;
  duration?: number; // Duration in seconds
}

const StarWarsCrawl: React.FC<StarWarsCrawlProps> = ({ text, onSkip, duration = 50 }) => {
  const audioRef = useRef<HTMLAudioElement | null>(null);
  const crawlContentRef = useRef<HTMLDivElement | null>(null);
  const previousContentHeightRef = useRef<number>(0);

  useEffect(() => {
    // Start the audio
    if (audioRef.current) {
      audioRef.current.play().catch((error) => {
        console.error('Error playing audio:', error);
      });
    }

    // Add animation end listener
    const handleAnimationEnd = () => {
      onSkip(); // Automatically end the crawl
    };

    const crawlContent = crawlContentRef.current;
    if (crawlContent) {
      crawlContent.addEventListener('animationend', handleAnimationEnd);
    }

    return () => {
      // Clean up event listener
      if (crawlContent) {
        crawlContent.removeEventListener('animationend', handleAnimationEnd);
      }
      // Stop the audio when the component unmounts
      if (audioRef.current) {
        audioRef.current.pause();
        audioRef.current.currentTime = 0;
      }
    };
  }, [onSkip]);

  useEffect(() => {
    // Adjust the transform when content changes
    const crawlContent = crawlContentRef.current;
    if (crawlContent) {
      const currentHeight = crawlContent.getBoundingClientRect().height;
      const previousHeight = previousContentHeightRef.current;

      if (previousHeight !== 0 && previousHeight !== currentHeight) {
        const heightDiff = currentHeight - previousHeight;

        // Get current transform
        const style = window.getComputedStyle(crawlContent);
        const matrix = new DOMMatrixReadOnly(style.transform);
        const translateY = matrix.m42;

        // Adjust translateY to compensate for the height difference
        const newTranslateY = translateY - heightDiff;

        // Apply the new transform
        crawlContent.style.transform = `translateX(-50%) translateY(${newTranslateY}px)`;
      }

      previousContentHeightRef.current = currentHeight;
    }
  }, [text]);

  return (
    <div className="star-wars-crawl">
      <div
        className="crawl-content"
        style={{ '--animation-duration': `${duration}s` } as React.CSSProperties}
        ref={crawlContentRef}
      >
        {text.map((paragraph, index) => (
          <p key={index} className={index === 0 ? 'title' : ''}>
            {paragraph}
          </p>
        ))}
      </div>
      <button className="skip-button btn btn-secondary" onClick={onSkip} aria-label="Skip Crawl">
        Skip
      </button>
      {/* Audio Element */}
      <audio ref={audioRef} src="/assets/star-wars-theme.mp3" />
    </div>
  );
};

export default StarWarsCrawl;
