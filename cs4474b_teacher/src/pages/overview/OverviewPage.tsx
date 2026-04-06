import clsx from "clsx";
import { Link, type To } from "react-router";

import type { GameSet } from "../../types/games";

import "./OverviewPage.css";

import { Button } from "../../components/Button";

function Metadata() {
  return <div className="Metadata"></div>;
}

function GameOverview({
  className,
  title,
  subtitle,
  linkTo,
}: {
  className?: string;
  title: string;
  subtitle: string;
  linkTo: To;
}) {
  return (
    <Link className={clsx("GameOverview", className)} to={linkTo}>
      <h2 className="GameOverview__title">{title}</h2>
      <p className="GameOverview__subtitle">{subtitle}</p>
      <span className="GameOverview__arrow">→</span>
    </Link>
  );
}

function GameOverviews({ gameSet }: { gameSet: GameSet }) {
  return (
    <div className="GameOverviews">
      <GameOverview
        title="Proofread"
        subtitle={`${gameSet.proofreadGames.length} paragraphs`}
        linkTo="/edit/proofread"
      />
      <GameOverview
        title="Homophones"
        subtitle={`${gameSet.homophoneGames.length} homophone sets`}
        linkTo="/edit/homophones"
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
  return (
    <div className="OverviewPage">
      <main className="OverviewPage__content">
        <Metadata />
        <GameOverviews gameSet={gameSet} />
        <Button variant="primary" size="large">Download</Button>
      </main>
    </div>
  );
}
