import * as React from "react";
import { ErrorBoundary, type FallbackProps } from "react-error-boundary";
import { BrowserRouter, Navigate, Route, Routes } from "react-router";

import { HomePage } from "../pages/home/HomePage";
import { HomophonesPage } from "../pages/homophones/HomophonesPage";
import { OverviewPage } from "../pages/overview/OverviewPage";
import { ProofreadPage } from "../pages/proofread/ProofreadPage";
import { TranscriptionPage } from "../pages/transcription/TranscriptionPage";
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
          <Route path="/" element={<HomePage onLoadGameSet={setGameSet} />} />
          {/* TODO: Figure out how to make this less repetitive */}
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
          <Route
            path="/edit/proofread"
            element={
              gameSet != null ? (
                <ProofreadPage gameSet={gameSet} onGameSetChange={setGameSet} />
              ) : (
                <Navigate to="/" replace />
              )
            }
          />
          <Route
            path="/edit/transcription"
            element={
              gameSet != null ? (
                <TranscriptionPage
                  gameSet={gameSet}
                  onGameSetChange={setGameSet}
                />
              ) : (
                <Navigate to="/" replace />
              )
            }
          />
          <Route
            path="/edit/homophones"
            element={
              gameSet != null ? (
                <HomophonesPage
                  gameSet={gameSet}
                  onGameSetChange={setGameSet}
                />
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
