import clsx from "clsx";

import "./TextInput.css";

export function TextInput({
  className,
  type = "text",
  ...props
}: React.ComponentPropsWithRef<"input">) {
  return (
    <input className={clsx("TextInput", className)} type={type} {...props} />
  );
}
