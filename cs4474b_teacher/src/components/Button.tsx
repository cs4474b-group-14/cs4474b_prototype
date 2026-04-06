/*
  A button.

  We could just use an HTML <button>, but using a <Button> component
  helps keeps things consistent if the style changes in the future.
*/

import clsx from "clsx";

import "./Button.css";

export type ButtonVariant = "primary" | "secondary";

export function Button<
  C extends
    | keyof React.JSX.IntrinsicElements
    | React.JSXElementConstructor<any>,
>({
  as: Component = "button",
  className,
  variant = "secondary",
  ...props
}: React.ComponentProps<C> & {
  as?: C;
  variant?: ButtonVariant;
}) {
  return (
    <Component
      className={clsx("Button", `Button--${variant}`, className)}
      {...props}
    />
  );
}
