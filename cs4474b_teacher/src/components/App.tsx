import { Counter } from "./Counter";

import "./App.css";

export const App = () => {
  return (
    <div className="App">
      <h1 className="App__title">Spelling Central</h1>
      <Counter className="App__counter" />
    </div>
  );
};
