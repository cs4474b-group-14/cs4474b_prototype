import type { GameSet } from "../../types/games";

import "./HomophonesPage.css";

export function HomophonesPage({
  gameSet,
  onGameSetChange = () => {},
}: {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}) {
  void gameSet;
  void onGameSetChange;

  return <div className="HomophonesPage">Placeholder!</div>;
}
