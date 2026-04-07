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
  name?: string;
  proofreadGames: ProofreadGame[];
  transcriptionGames: TranscriptionGame[];
  homophoneGames: HomophoneGame[];
  prioritizedWords: string[];
};

export const NEW_GAME_SET: GameSet = {
  name: "New Game Set",
  proofreadGames: [],
  transcriptionGames: [],
  homophoneGames: [],
  prioritizedWords: [],
};

export const areProofreadGamesValid = (gameSet: GameSet): boolean => {
  // This doesn't check for duplicates. Don't care right now
  return gameSet.proofreadGames.every(
    (game) => game.answerText.trim() !== "" && game.errorCount >= 1,
  );
};

export const areHomophoneGamesValid = (gameSet: GameSet): boolean => {
  // This doesn't check for duplicates. Don't care right now
  return gameSet.homophoneGames.every(
    (game) =>
      game.words.filter((w) => w.trim() !== "").length >= 2 &&
      game.exampleSentences.filter((s) => s.trim() !== "").length >= 1,
  );
};

export const isGameSetValid = (gameSet: GameSet): boolean =>
  areProofreadGamesValid(gameSet) && areHomophoneGamesValid(gameSet);
