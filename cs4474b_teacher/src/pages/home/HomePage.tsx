import { Link } from "react-router";

import { Button } from "../../components/Button";

import "./HomePage.css";

export function HomePage() {
  return (
    <div className="HomePage">
      <h1 className="HomePage__title">Spelling Central</h1>
      <div className="HomePage__buttons">
        <Button
          className="HomePage__button"
          variant="primary"
          as={Link}
          // TODO: Reset currently loaded data
          to="/edit"
        >
          New game set
        </Button>
        <Button
          className="HomePage__button"
          variant="secondary"
          as={Link}
          // TODO: Show a dialog to open a JSON file
          to="/edit"
        >
          Edit existing game set
        </Button>
      </div>
    </div>
  );
}
