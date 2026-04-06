export type ProofreadGame = {
  answerText: string;
  errorCount: number;
};

export type TranscriptionGame = {
  word: string;
  audioData: string; // Data URI: data:audio/ogg;codecs=opus;base64,T2dnUwACAAAAAAAAAA...
};

export type HomophoneGame = {
  id: string;
  words: string[];
  exampleSentences: string[];
};

export type GameSet = {
  proofreadGames: ProofreadGame[];
  transcriptionGames: TranscriptionGame[];
  homophoneGames: HomophoneGame[];
  prioritizedWords: string[];
};

export const EMPTY_GAME_SET: GameSet = {
  proofreadGames: [],
  transcriptionGames: [],
  homophoneGames: [],
  prioritizedWords: [],
};
