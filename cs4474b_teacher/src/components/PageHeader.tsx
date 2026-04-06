import clsx from "clsx";
import { Link, type To } from "react-router";

import "./PageHeader.css";

export type PageHeaderIcon = "back" | "close";

export function PageHeader({
  className,
  backIcon = "back",
  backLabel = "Back",
  backLinkTo,
}: {
  className?: string;
  backIcon?: PageHeaderIcon;
  backLabel?: string;
  backLinkTo: To;
}) {
  let iconCharacter: string;
  switch (backIcon) {
    case "back":
      iconCharacter = "←";
      break;
    case "close":
      iconCharacter = "×";
      break;
    default:
      throw new Error(`Invalid icon '${backIcon}'`);
  }

  return (
    <header className={clsx("PageHeader", className)}>
      <Link className="PageHeader__back-link" to={backLinkTo}>
        <span className="PageHeader__back-icon">{iconCharacter}</span>
        <span className="PageHeader__back-label">{backLabel}</span>
      </Link>
    </header>
  );
}
