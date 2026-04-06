import clsx from "clsx";
import { Link, type To } from "react-router";

import "./ArrowLink.css";

export function ArrowLink({
  className,
  title,
  subtitle,
  to: to,
}: {
  className?: string;
  title: string;
  subtitle: string;
  to: To;
}) {
  return (
    <Link className={clsx("ArrowLink", className)} to={to}>
      <h2 className="ArrowLink__title">{title}</h2>
      <p className="ArrowLink__subtitle">{subtitle}</p>
      <span className="ArrowLink__arrow">→</span>
    </Link>
  );
}
