import { Link } from "react-router";
import type { GameSet, HomophoneGame } from "../../types/games";
import { TextInput } from "../../components/TextInput";
import { Button } from "../../components/Button";
import "./HomophonesPage.css";

interface HomophonesPageProps {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}

// Each set has 2-4 words and 1-2 example sentences //
export function HomophonesPage({ gameSet, onGameSetChange }: HomophonesPageProps) {
  const sets = gameSet.homophoneGames || [];

  const pushUpdate = (updatedSets: HomophoneGame[]) => {
    onGameSetChange({ ...gameSet, homophoneGames: updatedSets });
  };

  const handleWordChange = (setId: string, wordIndex: number, newValue: string) => {
    const updatedSets = sets.map(s => {
      if (s.id === setId) {
        const newWords = [...s.words];
        newWords[wordIndex] = newValue.toLowerCase();
        return { ...s, words: newWords };
      }
      return s;
    });
    pushUpdate(updatedSets);
  };

  /**  
   * Setting word to empty string maintains  4-word grid structure and simplifies autofill logic, 
   * BUT we need to be careful to ignore empty strings in our JSON logic snd other checks. 
   * */
  const deleteWord = (setId: string, wordIndex: number) => {
    const updatedSets = sets.map(s => {
      if (s.id === setId) {
        const newWords = [...s.words];
        newWords[wordIndex] = ""; 
        return { ...s, words: newWords };
      }
      return s;
    });
    pushUpdate(updatedSets);
  };

  // AUTOFILL LOGIC: Fetches homophones from Datamuse API and fills grid with the typed word + suggestions
  const fetchHomophones = async (word: string, setId: string) => {
    if (!word) return;
    try {
      const response = await fetch(`https://api.datamuse.com/words?sl=${word}&max=4`);
      const data = await response.json();

      // Extract words and makes sure we don't include seed word itself
      const suggestedWords = data
        .map((item: any) => item.word.toLowerCase())
        .filter((w: string) => w !== word.toLowerCase());
      
      // Combine typed word with the first 3 suggestions
      const finalWords = [word.toLowerCase(), ...suggestedWords].slice(0, 4);

      // If API returns fewer than 4 (This will almost never happen), pad with empty strings so grid stays griddy
      while (finalWords.length < 4) finalWords.push("");

      pushUpdate(sets.map(s => (s.id === setId ? { ...s, words: finalWords } : s)));
    } catch (e) { 
      console.error("Autofill failed:", e); 
    }
  };

  // VALIDATION CHECKS
  const getDuplicateError = (currentSet: HomophoneGame, allSets: HomophoneGame[]) => {
    const activeWords = currentSet.words.filter(w => w.trim() !== "");
    if (activeWords.length === 0) return null;

    // Check for exact duplicate sets (ignoring order and empty strings)
    const currentSorted = [...activeWords].sort().join(",");
    const isDuplicateSet = allSets.some(s => {
      const sActive = s.words.filter(w => w.trim() !== "");
      return s.id !== currentSet.id && sActive.length > 0 && [...sActive].sort().join(",") === currentSorted;
    });
    return isDuplicateSet ? "This exact set already exists." : null;
  };

  const deleteSet = (setId: string) => {
    pushUpdate(sets.filter(s => s.id !== setId));
  };

  // Add confirmation check before clearing all sets, just in case
  const clearAllSets = () => {
    if (window.confirm("Are you sure you want to clear all sets? This cannot be undone.")) {
      pushUpdate([]);
    }
  };

  const handleSentenceChange = (setId: string, index: number, value: string) => {
    const updatedSets = sets.map(s => {
      if (s.id === setId) {
        const newSentences = [...(s.exampleSentences || ["", ""])];
        newSentences[index] = value;
        return { ...s, exampleSentences: newSentences };
      }
      return s;
    });
    pushUpdate(updatedSets);
  };

  // disable add new set button if last set is invalid (less than 2 words or is exact copy of other set)
  const isLastSetInvalid = sets.length > 0 && (
    sets[sets.length - 1].words.filter(w => w.trim() !== "").length < 2 ||
    getDuplicateError(sets[sets.length - 1], sets) !== null
  );

  return (
    <div className="HomophonesPage">
      <header className="HomophonesPage__header">
        <Link to="/edit" className="HomophonesPage__back-btn">
          <span style={{ fontWeight: 900, marginRight: '8px' }}>←</span>
          My Cool Word List
        </Link>
        {/* Only show clear all if there are actually sets to clear*/ }
        {sets.length > 0 && ( 
          <Button onClick={clearAllSets} size="normal" variant="secondary">
            Clear All Sets
          </Button>
        )}
      </header>

      <main className="HomophonesPage__list">
        {sets.map((set, index) => {
          const activeWords = set.words.filter(w => w.trim() !== "");
          const activeSentences = (set.exampleSentences || []).filter(s => s?.trim() !== "");
          
          const hasEnoughWords = activeWords.length >= 2;
          const hasSentence = activeSentences.length >= 1;
          const duplicateError = getDuplicateError(set, sets);
          const isIncomplete = !hasEnoughWords || !hasSentence || duplicateError !== null;

          // FEEDBACK STUFF: red for empty, yellow for partially filled, hidden when done
          const isEmpty = activeWords.length === 0 && activeSentences.length === 0;
          const isPartiallyFilled = !isEmpty && isIncomplete;

          return (
            <section 
              key={set.id} 
              className={`HomophoneCard ${isEmpty ? 'is-empty' : ''} ${isPartiallyFilled ? 'is-partial' : ''}`}
            >
              {isIncomplete && <div className="HomophoneCard__indicator" />}
              
              <div className="HomophoneCard__header">
                <h3 className="HomophoneCard__title">Set {String(index + 1).padStart(2, '0')}</h3>
                <button 
                  className="HomophoneCard__delete-btn" 
                  onClick={() => deleteSet(set.id)}
                  aria-label="Delete set"
                >
                  ×
                </button>
              </div>

              <div className="HomophoneCard__word-grid">
                {[0, 1, 2, 3].map((i) => {
                  const word = set.words[i] || "";
                  return (
                    <div key={i} className="WordInputWrapper">
                      <TextInput 
                        className="WordInputWrapper__input"
                        value={word}
                        placeholder={i === 0 ? "Type..." : "Empty..."}
                        onChange={(e) => handleWordChange(set.id, i, e.target.value)}
                        onKeyDown={(e) => e.key === 'Enter' && i === 0 && fetchHomophones(e.currentTarget.value, set.id)}
                      />
                      {word && (
                        <button className="WordInputWrapper__clear" onClick={() => deleteWord(set.id, i)}>×</button>
                      )}
                    </div>
                  );
                })}
              </div>

              <div className="HomophoneCard__sentences">
                <label className="HomophoneCard__label">Example Sentences</label>
                {[0, 1].map((i) => (
                  <textarea
                    key={i}
                    className="HomophoneCard__textarea"
                    placeholder={`Sentence ${i + 1}...`}
                    value={set.exampleSentences?.[i] || ""}
                    onChange={(e) => handleSentenceChange(set.id, i, e.target.value)}
                  />
                ))}
              </div>

              {isIncomplete && (
                <div className="HomophoneCard__errors">
                  {!hasEnoughWords && <p className="HomophoneCard__error-text">• Needs 2+ words</p>}
                  {!hasSentence && <p className="HomophoneCard__error-text">• Needs 1+ example sentences</p>}
                  {duplicateError && <p className="HomophoneCard__error-text">• {duplicateError}</p>}
                </div>
              )}
            </section>
          );
        })}

        <button 
          className="HomophonesPage__add-btn" 
          disabled={isLastSetInvalid}
          onClick={() => pushUpdate([...sets, { 
            id: Date.now().toString(), 
            words: ["", "", "", ""], 
            exampleSentences: ["", ""] 
          }])}
        >
          + Add New Set
        </button>
      </main>
    </div>
  );
}