import clsx from "clsx";
import * as React from "react";

import "./Counter.css";

// Example component to demonstrate coding conventions
export const Counter = ({ className = "" }: { className?: string }) => {
  const [count, setCount] = React.useState(0);

  return (
    <div className={clsx("Counter", className)}>
      <p className="Counter__text">Count is {count}</p>
      <button className="Counter__button" onClick={() => setCount(count + 1)}>
        Increment
      </button>
    </div>
  );
};
