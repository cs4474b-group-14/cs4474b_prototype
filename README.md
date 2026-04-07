# Spelling Central

This repository contains the prototype implementation of our reconceptualization
of the Spelling Central app. It is split into two parts: a _student_ app and a
_teacher_ app. The _teacher_ app is used to create "game sets" that can then be
loaded into the _student_ app.

## Student

The student app is built in Unity 2022.3.62f1.

## Teacher

The teacher app is a single-page web application (SPA) written in React +
TypeScript and built with Vite and pnpm.

To build the teacher app, you will need the following dependencies installed:

 -  A reasonably recent version of [Node.js][]. Version 24.14.1 is recommended.
 -  A reasonably recent version of [pnpm][]. Version 10.33.0 is recommended.

The app can be run in one of two ways:

 -  Build for production and serve the resulting HTML, CSS, and JavaScript
 -  Run a live development server

### Building for production

To create a production build of the teacher app, open a terminal in the
`cs4474b_teacher` directory and run:

```sh
pnpm run build
```

This will produce HTML, CSS, and JavaScript files in the `dist` directory. To
run the app using these files, you must serve them with a static HTTP server
that renders `index.html` at `/`.

The following HTTP server commands are known to work correctly:
 -  `python3 -m http.server`
 -  `pnpx http-server`
 -  `php -S localhost:8080`

**Opening `index.html` directly in the browser WILL NOT WORK.**

### Running a development server

To run a live development server, open a terminal in the `cs4474b_teacher`
directory and run:

```sh
pnpm run dev
```

The app should now be accessible at http://localhost:5173/. Any changes you make
to the app's source code will be automatically reloaded.


[Node.js]: https://nodejs.org/en
[pnpm]: https://pnpm.io/installation

<!-- vim:sw=4 et: -->
