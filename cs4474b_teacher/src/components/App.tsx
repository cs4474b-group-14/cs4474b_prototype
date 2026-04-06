import * as React from "react";
import { ErrorBoundary, type FallbackProps } from "react-error-boundary";
import { BrowserRouter, Navigate, Route, Routes } from "react-router";

import { HomePage } from "../pages/home/HomePage";
import { OverviewPage } from "../pages/overview/OverviewPage";
import type { GameSet } from "../types/games";

import "./App.css";

function Error({ error }: FallbackProps) {
  return (
    <div className="Error">
      <p>Oh no! We ran into an error:</p>
      <pre>
        <code>{String(error)}</code>
      </pre>
    </div>
  );
}

export function App() {
  const [gameSet, setGameSet] = React.useState<GameSet>();

  return (
    <ErrorBoundary FallbackComponent={Error}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route
            path="/edit"
            element={
              gameSet != null ? (
                <OverviewPage gameSet={gameSet} onGameSetChange={setGameSet} />
              ) : (
                <Navigate to="/" replace />
              )
            }
          />
        </Routes>
      </BrowserRouter>
    </ErrorBoundary>
  );
}
