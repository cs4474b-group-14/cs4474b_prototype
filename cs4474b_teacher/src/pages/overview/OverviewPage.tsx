import clsx from "clsx";

import { ArrowLink } from "../../components/ArrowLink";
import { Button } from "../../components/Button";
import { BackButton, PageHeader } from "../../components/PageHeader";
import { TextInput } from "../../components/TextInput";
import {
  areHomophoneGamesValid,
  areProofreadGamesValid,
  isGameSetValid,
  type GameSet,
} from "../../types/games";

import "./OverviewPage.css";

function Metadata({
  gameSet,
  onGameSetChange = () => {},
}: {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}) {
  return (
    <div className="Metadata">
      <label className="Metadata__field">
        <span className="Metadata__label">Name</span>
        <TextInput
          value={gameSet.name ?? ""}
          onChange={(e) =>
            onGameSetChange({
              ...gameSet,
              name: e.target.value,
            })
          }
        />
      </label>
    </div>
  );
}

function GameOverviews({ gameSet }: { gameSet: GameSet }) {
  return (
    <div className="GameOverviews">
      <ArrowLink
        className={clsx(
          "GameOverviews__link",
          !areProofreadGamesValid(gameSet) && "GameOverviews__link--invalid",
        )}
        title="Proofread"
        subtitle={`${gameSet.proofreadGames.length} paragraphs`}
        to="/edit/proofread"
      />
      <ArrowLink
        className={clsx(
          "GameOverviews__link",
          !areHomophoneGamesValid(gameSet) && "GameOverviews__link--invalid",
        )}
        title="Homophones"
        subtitle={`${gameSet.homophoneGames.length} homophone sets`}
        to="/edit/homophones"
      />
      <ArrowLink
        title="Transcription"
        subtitle={`${gameSet.transcriptionGames.length} words with audio`}
        to="/edit/transcription"
      />
    </div>
  );
}

export function OverviewPage({
  gameSet,
  onGameSetChange = () => {},
}: {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}) {
  const handleDownload = () => {
    const json = JSON.stringify(gameSet);

    // https://coreui.io/answers/how-to-download-a-file-in-javascript/
    const blob = new Blob([json], { type: "application/json" });
    const url = URL.createObjectURL(blob);

    const link = document.createElement("a");
    link.href = url;
    link.download = `${gameSet.name}.json`;

    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    URL.revokeObjectURL(url);
  };

  const isValid = isGameSetValid(gameSet);

  return (
    <div className="OverviewPage">
      {/* TODO: Add confirmation before closing game set */}
      <PageHeader className="OverviewPage__header">
        <BackButton icon="close" linkTo="/">
          Close
        </BackButton>
      </PageHeader>
      <main className="OverviewPage__content">
        <Metadata gameSet={gameSet} onGameSetChange={onGameSetChange} />
        <GameOverviews gameSet={gameSet} />
        <Button
          variant="primary"
          size="large"
          onClick={handleDownload}
          disabled={!isValid}
        >
          Download
        </Button>
        {!isValid && (
          <p className="OverviewPage__error">
            Some games are invalid. Please fix the errors before downloading.
          </p>
        )}
      </main>
    </div>
  );
}
