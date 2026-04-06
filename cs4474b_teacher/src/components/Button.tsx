/*
  A button.

  We could just use an HTML <button>, but using a <Button> component
  helps keeps things consistent if the style changes in the future.
*/

import clsx from "clsx";

import "./Button.css";

export type ButtonVariant = "primary" | "secondary";

export const Button = ({
  className,
  variant = "secondary",
  ...props
}: React.ComponentProps<"button"> & {
  variant?: ButtonVariant;
}) => {
  return (
    <button
      className={clsx("Button", `Button--${variant}`, className)}
      {...props}
    />
  );
};
