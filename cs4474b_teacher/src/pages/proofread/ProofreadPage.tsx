import type { GameSet } from "../../types/games";

import "./ProofreadPage.css";

export function ProofreadPage({
  gameSet,
  onGameSetChange = () => {},
}: {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}) {
  void gameSet;
  void onGameSetChange;

  return <div className="ProofreadPage">Placeholder!</div>;
}
