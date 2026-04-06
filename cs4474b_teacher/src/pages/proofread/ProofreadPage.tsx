import * as React from "react";

import type { GameSet, ProofreadGame } from "../../types/games";
import { BackButton, PageHeader } from "../../components/PageHeader";
import { HighlightedTextarea } from "../../components/HighlightedTextarea";

import "./ProofreadPage.css";

interface ProofReadPageProps {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}

function normalizeParagraphs(games: ProofreadGame[]): ProofreadGame[] {
  const nonEmptyGames = games.filter(
    (game) => game.answerText.trim() !== "" || game.errorCount > 0
  );

  return [...nonEmptyGames, { answerText: "", errorCount: 1 }];
}

function normalizePrioritizedWords(words: string[]): string[] {
  const nonEmptyWords = words.map((w) => w.trim()).filter((w) => w !== "");
  return [...nonEmptyWords, ""];
}

export function ProofreadPage({ gameSet, onGameSetChange, }: ProofReadPageProps ) {
  const [games, setGames] = React.useState<ProofreadGame[]>(
    gameSet.proofreadGames.length > 0
    ? normalizeParagraphs(gameSet.proofreadGames)
    : [{ answerText: "", errorCount: 1}]
  );

  const [prioritizedWords, setPrioritizedWords] = React.useState<string[]>(
    gameSet.prioritizedWords.length > 0
    ? normalizePrioritizedWords(gameSet.prioritizedWords)
    : [""]
  );

  const syncGameSet = (
    updatedGames: ProofreadGame[],
    updatedWords: string[]
  ) => {
    const normalizedGames = normalizeParagraphs(updatedGames);
    const normalizedWords = normalizePrioritizedWords(updatedWords);

    setGames(normalizedGames);
    setPrioritizedWords(normalizedWords);

    onGameSetChange({
      ...gameSet,
      proofreadGames: normalizedGames.filter(
        (game) => game.answerText.trim() !== ""
      ),
      prioritizedWords: normalizedWords.filter((word) => word.trim() !== ""),
    });
  };

  const updateGames = (updatedGames: ProofreadGame[]) => {
    syncGameSet(updatedGames, prioritizedWords);
  };

  const updateWords = (updatedWords: string[]) => {
    syncGameSet(games, updatedWords);
  };

  const handleParagraphChange = (index: number, newValue: string) => {
    const updatedGames = [...games];
    updatedGames[index] = {
      ...updatedGames[index],
      answerText: newValue,
    };
    updateGames(updatedGames);
  };

  const handleErrorCountChange = (index: number, newValue: string) => {
    const parsed = Math.max(0, Number(newValue) || 0);
    const updatedGames = [...games];
    updatedGames[index] = {
      ...updatedGames[index],
      errorCount: parsed,
    };
    updateGames(updatedGames);
  };

  const deleteParagraph = (index: number) => {
    const updatedGames = games.filter((_, i) => i !== index);
    updateGames(updatedGames);
  };

  const handlePrioritizedWordChange = (index: number, newValue: string) => {
    const updatedWords = [...prioritizedWords];
    updatedWords[index] = newValue.toLowerCase();
    updateWords(updatedWords);
  };

  const deletePrioritizedWord = (index: number) => {
    const updatedWords = prioritizedWords.filter((_, i) => i !== index);
    updateWords(updatedWords);
  };

  const getDuplicateParagraphError = (
    currentGame: ProofreadGame,
    currentIndex: number
  ) => {
    const currentText = currentGame.answerText.trim().toLowerCase();
    if (!currentText) return null;

    const duplicateExists = games.some(
      (game, index) =>
        index !== currentIndex &&
        game.answerText.trim().toLowerCase() === currentText
    );

    return duplicateExists ? "This exact paragraph already exists." : null;
  };

  const activeGames = games;
  const activeWords = prioritizedWords;

  return (
    <div className="ProofReadPage">
      <div className="ProofReadPage__header">
        <PageHeader className="OverviewPage__header">
          <BackButton icon="back" linkTo="/">
            Back
          </BackButton>
      </PageHeader>

        <h1 className="ProofReadPage__title">Proofread Games</h1>
      </div>

        <div className="ProofReadPage__layout">
        <section className="ProofReadPage__panel">
          <div className="ProofReadPage__panel-header">
            <h2 className="ProofReadPage__section-title">Paragraphs</h2>
            <p className="ProofReadPage__section-text">
              Each paragraph will be one proofread entry.
            </p>
          </div>

          <div className="ProofReadPage__panel-body ProofReadPage__panel-body--scroll">
            <div className="ProofReadPage__paragraph-list">
              {activeGames.map((game, index) => {
                const isTrailingEmpty =
                  index === activeGames.length - 1 &&
                  game.answerText.trim() === "";

                const duplicateError = getDuplicateParagraphError(game, index);
                const hasParagraph = game.answerText.trim() !== "";
                const hasValidErrorCount = game.errorCount >= 1;

                return (
                  <article key={index} className="ProofReadPage__card">
                    <div className="ProofReadPage__card-header">
                      <h3 className="ProofReadPage__card-title">
                        {isTrailingEmpty
                          ? "New paragraph"
                          : `Paragraph ${String(index + 1).padStart(2, "0")}`}
                      </h3>

                      {!isTrailingEmpty && (
                        <button
                          className="ProofReadPage__delete"
                          type="button"
                          onClick={() => deleteParagraph(index)}
                          aria-label={`Delete paragraph ${index + 1}`}
                        >
                          ×
                        </button>
                      )}
                    </div>

                    <label className="ProofReadPage__label">
                      Correct paragraph
                      <HighlightedTextarea
                        className="ProofReadPage__textarea"
                        value={game.answerText}
                        words={prioritizedWords}
                        placeholder={
                          isTrailingEmpty
                            ? "Type to add a new paragraph..."
                            : "Type or paste the correct paragraph here..."
                        }
                        onChange={(value) => handleParagraphChange(index, value)}
                      />
                    </label>

                    <label className="ProofReadPage__label">
                      Number of spelling errors to generate
                      <input
                        className="ProofReadPage__input ProofReadPage__number-input"
                        type="number"
                        min={0}
                        value={game.errorCount}
                        onChange={(e) =>
                          handleErrorCountChange(index, e.target.value)
                        }
                      />
                    </label>

                    {!isTrailingEmpty &&
                      (!hasParagraph ||
                        !hasValidErrorCount ||
                        duplicateError) && (
                        <div className="ProofReadPage__warning">
                          {!hasParagraph && <p>• Add a paragraph</p>}
                          {!hasValidErrorCount && (
                            <p>• Error count must be at least 1</p>
                          )}
                          {duplicateError && <p>• {duplicateError}</p>}
                        </div>
                      )}
                  </article>
                );
              })}
            </div>
          </div>
        </section>

        <section className="ProofReadPage__panel">
          <div className="ProofReadPage__panel-header">
            <h2 className="ProofReadPage__section-title">Focused Words</h2>
            <p className="ProofReadPage__section-text">
              These words are more likely to be misspelled and are shared across the game set.
            </p>
          </div>

          <div className="ProofReadPage__panel-body ProofReadPage__panel-body--scroll">
            <div className="ProofReadPage__word-list">
              {activeWords.map((word, index) => {
                const isTrailingEmpty =
                  index === activeWords.length - 1 && word.trim() === "";

                return (
                  <div key={index} className="ProofReadPage__word-item">
                    <input
                      className="ProofReadPage__input"
                      type="text"
                      value={word}
                      placeholder={
                        isTrailingEmpty
                          ? "Type to add a new focus word..."
                          : "Focus word"
                      }
                      onChange={(e) =>
                        handlePrioritizedWordChange(index, e.target.value)
                      }
                    />

                    {!isTrailingEmpty && (
                      <button
                        className="ProofReadPage__delete"
                        type="button"
                        onClick={() => deletePrioritizedWord(index)}
                        aria-label={`Delete focused word ${index + 1}`}
                      >
                        ×
                      </button>
                    )}
                  </div>
                );
              })}
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}
