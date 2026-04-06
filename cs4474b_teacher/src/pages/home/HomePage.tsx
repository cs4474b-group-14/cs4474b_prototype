import * as React from "react";
import { useNavigate } from "react-router";

import { Button } from "../../components/Button";
import { NEW_GAME_SET, type GameSet } from "../../types/games";

import "./HomePage.css";

import { ArrowLink } from "../../components/ArrowLink";

export function HomePage({
  gameSet,
  onGameSetChange: onGameSetChange = () => {},
}: {
  gameSet?: GameSet | undefined;
  onGameSetChange?: (gameSet: GameSet) => void;
}) {
  const navigate = useNavigate();
  const filePickerRef = React.useRef<HTMLInputElement | null>(null);

  const handleNew = () => {
    onGameSetChange(NEW_GAME_SET);
    navigate("/edit");
  };
  const handleOpen = async (
    e: React.ChangeEvent<HTMLInputElement, HTMLInputElement>,
  ) => {
    // We always attach this event handler to an input of type="file",
    // so this will never be null
    const files = e.currentTarget.files!;
    const file = files[0];
    // TODO: Show loading state visually
    const fileText = await file.text();

    // We'll just trust that the file has the right structure. It's a prototype.
    const gameSet = JSON.parse(fileText) as GameSet;
    onGameSetChange(gameSet);
    navigate("/edit");
  };

  return (
    <div className="HomePage">
      <div className="HomePage__content">
        <h1 className="HomePage__title">Spelling Central</h1>
        {gameSet != null && (
          <ArrowLink
            className="HomePage__continue"
            title="Continue editing?"
            subtitle={`When you left off, you were editing ${gameSet.name}`}
            to="/edit"
          />
        )}
        <div className="HomePage__buttons">
          <Button
            className="HomePage__button"
            variant="primary"
            size="large"
            onClick={handleNew}
          >
            New game set
          </Button>
          <Button
            className="HomePage__button"
            variant="secondary"
            size="large"
            onClick={() => filePickerRef.current?.click()}
          >
            Open existing game set
          </Button>
          {/* This will be hidden by CSS and triggered by the above button */}
          <input
            ref={filePickerRef}
            className="HomePage__file-picker"
            type="file"
            accept=".json,application/json"
            onChange={handleOpen}
          />
        </div>
      </div>
    </div>
  );
}
