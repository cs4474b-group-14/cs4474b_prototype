import clsx from "clsx";
import * as React from "react";
import { Link, type To } from "react-router";

import "./PageHeader.css";

export type BackButtonIcon = "back" | "close";

export function BackButton({
  className,
  linkTo,
  icon = "back",
  children = "Back",
}: {
  className?: string;
  linkTo: To;
  icon?: BackButtonIcon;
  children?: React.ReactNode;
}) {
  let iconCharacter: string;
  switch (icon) {
    case "back":
      iconCharacter = "←";
      break;
    case "close":
      iconCharacter = "×";
      break;
    default:
      throw new Error(`Invalid icon '${icon}'`);
  }

  return (
    <Link className={clsx("BackButton", className)} to={linkTo}>
      <span
        className={clsx(
          "BackButton__icon",
          // The × icon needs special alignment tweaks
          icon === "close" && "BackButton__icon--close",
        )}
      >
        {iconCharacter}
      </span>
      <span className="BackButton__label">{children}</span>
    </Link>
  );
}

export function PageHeader({
  className,
  children,
}: {
  className?: string;
  children?: React.ReactNode;
}) {
  return <header className={clsx("PageHeader", className)}>{children}</header>;
}
