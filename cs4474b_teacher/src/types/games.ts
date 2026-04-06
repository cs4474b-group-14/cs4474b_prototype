export type ProofreadGame = {
  answerText: string;
  errorCount: number;
};

export type TranscriptionGame = {
  word: string;
  audioData: string; // Data URI: data:audio/ogg;codecs=opus;base64,T2dnUwACAAAAAAAAAA...
};

export type HomophoneGame = {
  words: string[];
};

export type GameSet = {
  proofreadGames: ProofreadGame[];
  transcriptionGames: TranscriptionGame[];
  homophoneGames: TranscriptionGame[];
  prioritizedWords: string[];
};
