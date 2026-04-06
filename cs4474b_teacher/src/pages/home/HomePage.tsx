import * as React from "react";
import { useNavigate } from "react-router";

import { Button } from "../../components/Button";
import { EMPTY_GAME_SET, type GameSet } from "../../types/games";

import "./HomePage.css";

export function HomePage({
  onLoadGameSet = () => {},
}: {
  onLoadGameSet?: (gameSet: GameSet) => void;
}) {
  const navigate = useNavigate();
  const filePickerRef = React.useRef<HTMLInputElement | null>(null);

  const handleNew = () => {
    onLoadGameSet(EMPTY_GAME_SET);
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
    const fileText = await file.text()

    // We'll just trust that the file has the right structure. It's a prototype.
    const gameSet = JSON.parse(fileText) as GameSet;
    onLoadGameSet(gameSet);
    navigate("/edit");
  };

  return (
    <div className="HomePage">
      <h1 className="HomePage__title">Spelling Central</h1>
      <div className="HomePage__buttons">
        <Button
          className="HomePage__button"
          variant="primary"
          onClick={handleNew}
        >
          New game set
        </Button>
        <Button
          className="HomePage__button"
          variant="secondary"
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
  );
}
