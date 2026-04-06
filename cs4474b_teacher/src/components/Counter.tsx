import clsx from "clsx";
import * as React from "react";

import { Button } from "./Button";

import "./Counter.css";

// Example component to demonstrate coding conventions
export function Counter({ className }: { className?: string }) {
  const [count, setCount] = React.useState(0);

  return (
    <div className={clsx("Counter", className)}>
      <p className="Counter__text">Count is {count}</p>
      <Button
        className="Counter__button"
        variant="primary"
        onClick={() => setCount(count + 1)}
      >
        Increment
      </Button>
    </div>
  );
}
