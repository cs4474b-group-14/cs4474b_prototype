import clsx from "clsx";
import * as React from "react";

import "./App.css";

// Example component to demonstrate coding conventions
const Counter = ({ className = "" }: { className?: string }) => {
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

export const App = () => {
  return (
    <div className="App">
      <h1 className="App__title">Spelling Central</h1>
      <Counter className="App__counter" />
    </div>
  );
};
