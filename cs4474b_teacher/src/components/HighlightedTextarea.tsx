import * as React from "react";
import "./HighlightedTextarea.css";

interface HighlightedTextareaProps {
  value: string;
  words: string[];
  placeholder?: string;
  className?: string;
  onChange: (value: string) => void;
}

function escapeRegExp(value: string) {
  return value.replace(/[.*+?^${}()|[\]\\]/g, "\\$&");
}

function renderHighlightedText(text: string, words: string[]) {
  const activeWords = words
    .map((word) => word.trim())
    .filter((word) => word !== "")
    .sort((a, b) => b.length - a.length);

  if (!text || activeWords.length === 0) {
    return text || " ";
  }

  const escapedWords = activeWords.map(escapeRegExp);
  const regex = new RegExp(`\\b(${escapedWords.join("|")})\\b`, "gi");
  const parts = text.split(regex);

  return parts.map((part, index) => {
    const isMatch = activeWords.some(
      (word) => word.toLowerCase() === part.toLowerCase()
    );

    return isMatch ? (
      <mark key={index} className="HighlightedTextarea__mark">
        {part}
      </mark>
    ) : (
      <React.Fragment key={index}>{part}</React.Fragment>
    );
  });
}

export function HighlightedTextarea({
  value,
  words,
  placeholder,
  className = "",
  onChange,
}: HighlightedTextareaProps) {
  const textareaRef = React.useRef<HTMLTextAreaElement | null>(null);
  const backdropRef = React.useRef<HTMLDivElement | null>(null);

  const syncScroll = () => {
    if (!textareaRef.current || !backdropRef.current) return;
    backdropRef.current.scrollTop = textareaRef.current.scrollTop;
    backdropRef.current.scrollLeft = textareaRef.current.scrollLeft;
  };

  return (
    <div className={`HighlightedTextarea ${className}`}>
      {!value && placeholder ? (
        <div className="HighlightedTextarea__placeholder">{placeholder}</div>
      ) : null}

      <div
        ref={backdropRef}
        className="HighlightedTextarea__backdrop"
        aria-hidden="true"
      >
        <div className="HighlightedTextarea__content">
            {renderHighlightedText(value || " ", words)}
        </div>
      </div>

      <textarea
        ref={textareaRef}
        className="HighlightedTextarea__input"
        value={value}
        onChange={(e) => onChange(e.target.value)}
        onScroll={syncScroll}
        spellCheck={false}
      />
    </div>
  );
}