import type { GameSet } from "../../types/games";

function Metadata() {
  return <div className="Metadata"></div>;
}

function GameOverviews({
  gameSet,
  onGameSetChange = () => {},
}: {
  gameSet: GameSet;
  onGameSetChange: (gameSet: GameSet) => void;
}) {
  return (
    <div className="GameOverviews">
      {/* TODO: Make this an actual UI */}
      <pre>
        <code>
          {gameSet.proofreadGames.length} proofread games
          {gameSet.transcriptionGames.length} transcription games
          {gameSet.homophoneGames.length} homophone games
        </code>
      </pre>
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
  return (
    <div className="OverviewPage">
      <Metadata />
      <GameOverviews gameSet={gameSet} onGameSetChange={onGameSetChange} />
    </div>
  );
}
