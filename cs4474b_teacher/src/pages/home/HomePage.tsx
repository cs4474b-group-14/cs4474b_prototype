import * as React from "react";
import { useNavigate } from "react-router";

import { Button } from "../../components/Button";

import "./HomePage.css";

import { EMPTY_GAME_SET, type GameSet } from "../../types/games";

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
  const handleOpen = (
    e: React.ChangeEvent<HTMLInputElement, HTMLInputElement>,
  ) => {
    console.log("open", e);
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
          onChange={handleOpen}
        />
      </div>
    </div>
  );
}
