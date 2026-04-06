/*
  A button.

  We could just use an HTML <button>, but using a <Button> component
  helps keeps things consistent if the style changes in the future.
*/

import clsx from "clsx";

import "./Button.css";

export type ButtonVariant = "primary" | "secondary";

export function Button<C extends React.ElementType = "button">({
  as,
  className,
  variant = "secondary",
  ...props
}: React.ComponentPropsWithoutRef<C> & {
  as?: C;
  variant?: ButtonVariant;
}) {
  const Component = as ?? "button";
  return (
    <Component
      className={clsx("Button", `Button--${variant}`, className)}
      {...props}
    />
  );
}
