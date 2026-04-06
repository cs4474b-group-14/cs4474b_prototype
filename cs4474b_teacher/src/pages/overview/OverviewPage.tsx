function Metadata() {
  return <div className="Metadata"></div>;
}

function GameOverviews() {
  return <div className="GameOverviews"></div>;
}

export function OverviewPage() {
  return (
    <div className="OverviewPage">
      <Metadata />
      <GameOverviews />
    </div>
  );
}
