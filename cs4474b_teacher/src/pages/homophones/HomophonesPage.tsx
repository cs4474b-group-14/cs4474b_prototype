import * as React from "react";
import { Link } from "react-router";
import type { GameSet, HomophoneGame } from "../../types/games";
import "./HomophonesPage.css";

interface HomophonesPageProps {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}

// Each set has 2-4 words and 1-2 example sentences //
export function HomophonesPage({ gameSet, onGameSetChange }: HomophonesPageProps) {
  const [sets, setSets] = React.useState<HomophoneGame[]>(
    gameSet.homophoneGames.length > 0 
      ? gameSet.homophoneGames 
      : [{ id: "1", words: ["their", "there", "they're"], exampleSentences: ["They read their book"] }]
  );


  const updateAllSets = (updatedSets: HomophoneGame[]) => {
    setSets(updatedSets);
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
    updateAllSets(updatedSets);
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
    updateAllSets(updatedSets);
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
      while (finalWords.length < 4) {
        finalWords.push("");
      }

      updateAllSets(sets.map(s => (s.id === setId ? { ...s, words: finalWords } : s)));
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
    if (isDuplicateSet) return "This exact set already exists.";

    // Check for any overlapping words with other sets (ignoring empty strings)
    // THIS WAS REMOVED BECAUSE I REALIZED THE TEACHER MIGHT WANT DIFFERENT SENTENCES USING SAME HOMOPHONE, kept just in case
    /**  
    const otherWords = new Set(allSets.filter(s => s.id !== currentSet.id).flatMap(s => s.words.filter(w => w !== "")));
    if (activeWords.some(w => otherWords.has(w.toLowerCase()))) return "Words overlap with another set.";
    */

    return null;
  };


  const deleteSet = (setId: string) => {
    updateAllSets(sets.filter(s => s.id !== setId));
  };

  // Add confirmation check before clearing all sets, just in case
  const clearAllSets = () => {
    if (window.confirm("Are you sure you want to clear all sets? This cannot be undone.")) updateAllSets([]);
  };

  // disable add new set button if last set is invalid (less than 2 words or is exact copy of other set)
  const isLastSetInvalid = sets.length > 0 && (
    sets[sets.length - 1].words.filter(w => w.trim() !== "").length < 2 ||
    getDuplicateError(sets[sets.length - 1], sets) !== null
  );


  // CARD FLIP / EXAMPLE SENTENCE LOGIC
  const [isFlipped, setIsFlipped] = React.useState(false);

  const handleSentenceChange = (setId: string, index: number, value: string) => {
    const updatedSets = sets.map(s => {
      if (s.id === setId) {
        const newSentences = [...(s.exampleSentences || ["", ""])];
        newSentences[index] = value;
        return { ...s, exampleSentences: newSentences };
      }
      return s;
    });
    updateAllSets(updatedSets);
  };


return (
    <div className="HomophonesContainer">
      <header className="HomophonesHeader">
        <div className="HeaderLeft">
          <Link to="/edit" className="BackButton">← My Cool Word List</Link>
          <button 
            className={`ViewToggle ${isFlipped ? "active" : ""}`} 
            onClick={() => setIsFlipped(!isFlipped)}
          >
            { /* Handy emojis, good for visual cues */ }
            {isFlipped ? "✎ Edit Words" : "📝 Add Sentences"}
          </button>
        </div>

        {/* Only show clear all if there are actually sets to clear*/ }
        {sets.length > 0 && (
          <button className="ClearAllBtn" onClick={clearAllSets}>
            Clear All Sets
          </button>
        )}
      </header>
      

      <main className="HomophonesList">
        {sets.map((set, index) => {
          const hasEnoughWords = set.words.filter(w => w.trim() !== "").length >= 2;
          const hasSentence = set.exampleSentences && set.exampleSentences.some(s => s?.trim() !== "");
          const duplicateError = getDuplicateError(set, sets);
          const isIncomplete = !hasEnoughWords || !hasSentence || duplicateError !== null;

          return (
            <section key={set.id} className={`HomophoneSet ${isIncomplete ? "incomplete" : ""}`}>
              <div className="SetHeader">
                <h3 className="SetTitle">
                  {isFlipped 
                    ? `Sentences: ${set.words[0] || "???"}` 
                    : `Set ${String(index + 1).padStart(2, '0')}`
                  }
                </h3>
                {/* 
                Delete button for each set, maybe add confirmation later if we think accidental deletes are a problem.
                Also the multiplication sign works as a really great icon for delete buttons.
                */ }
                <button className="DeleteSetBtn" onClick={() => deleteSet(set.id)} title="Delete set">
                  ×
                </button>
              </div>

              {!isFlipped ? (
                /* FRONT: WORD INPUT */
                <div className="WordGrid">
                  {[0, 1, 2, 3].map((i) => {
                    const word = set.words[i] || "";
                    return (
                      <div key={i} className="WordWrapper">
                        <input 
                          className={`WordBox ${word ? "active" : "empty"}`} 
                          value={word}
                          placeholder={i === 0 ? "Type to start..." : "Empty..."}
                          onChange={(e) => handleWordChange(set.id, i, e.target.value)}
                          onKeyDown={(e) => {
                            if (e.key === 'Enter' && i === 0) {
                              fetchHomophones(e.currentTarget.value, set.id);
                            }
                          }}
                        />
                        {word && (
                          <button className="DeleteWordBtn" onClick={() => deleteWord(set.id, i)}>
                            ×
                          </button>
                        )}
                      </div>
                    );
                  })}
                </div>
              ) : (
                /* BACK: SENTENCE INPUT */
                <div className="SentenceList">
                  {[0, 1].map((i) => ( /* LIMITS to 2 sentences, change this array if we want more */
                    <textarea
                      key={i}
                      className="SentenceInput"
                      placeholder={`Example sentence ${i + 1}...`}
                      value={set.exampleSentences?.[i] || ""}
                      onChange={(e) => handleSentenceChange(set.id, i, e.target.value)}
                    />
                  ))}
                </div>
              )}

              <div className="ValidationWrapper">
                {isIncomplete && (
                  <div className="WarningBox">
                    {!hasEnoughWords && <p className="WarningText">• Add 2+ words</p>}
                    {!hasSentence && <p className="WarningText">• Add at least 1 sentence</p>}
                    {duplicateError && <p className="ErrorText">• {duplicateError}</p>}
                  </div>
                )}
              </div>
            </section>
          );
        })}

        <button 
          className="AddSetButton" 
          disabled={isLastSetInvalid}
          onClick={() => updateAllSets([...sets, { 
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