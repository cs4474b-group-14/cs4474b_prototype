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
  exampleSentences: string[];
};

export type GameSet = {
  name?: string;
  proofreadGames: ProofreadGame[];
  transcriptionGames: TranscriptionGame[];
  homophoneGames: TranscriptionGame[];
  prioritizedWords: string[];
};

export const NEW_GAME_SET: GameSet = {
  name: "New Game Set",
  proofreadGames: [],
  transcriptionGames: [],
  homophoneGames: [],
  prioritizedWords: [],
};
