import { BackButton, PageHeader } from "../../components/PageHeader";
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

  return (
    <div className="TranscriptionPage">
      <PageHeader className="TranscriptionPage__header">
        <BackButton icon="back" linkTo="/edit">
          {gameSet.name ?? "Back"}
        </BackButton>
      </PageHeader>
      <div className="TranscriptionPage__content"></div>
    </div>
  );
}
