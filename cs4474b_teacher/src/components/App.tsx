import { ErrorBoundary, type FallbackProps } from "react-error-boundary";
import { BrowserRouter, Route, Routes } from "react-router";

import { HomePage } from "../pages/home/HomePage";
import { OverviewPage } from "../pages/overview/OverviewPage";

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
  return (
    <ErrorBoundary FallbackComponent={Error}>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/edit" element={<OverviewPage />} />
        </Routes>
      </BrowserRouter>
    </ErrorBoundary>
  );
}
