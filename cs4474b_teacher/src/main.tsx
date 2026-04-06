import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { BrowserRouter, Route, Routes } from "react-router";

import { HomePage } from "./pages/home/HomePage.tsx";
import { OverviewPage } from "./pages/overview/OverviewPage.tsx";

import "./index.css";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/edit" element={<OverviewPage />} />
      </Routes>
    </BrowserRouter>
  </StrictMode>,
);
