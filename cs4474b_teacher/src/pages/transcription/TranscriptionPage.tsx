import type { GameSet } from "../../types/games";

import "./TranscriptionPage.css";

export function TranscriptionPage({
  gameSet,
  onGameSetChange = () => {},
}: {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}) {
  void gameSet;
  void onGameSetChange;

  return <div className="TranscriptionPage">Placeholder!</div>;
}
