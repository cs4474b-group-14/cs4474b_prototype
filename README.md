# Spelling Central

This repository contains the prototype implementation of our reconceptualization
of the Spelling Central app. It is split into two parts: a _student_ app and a
_teacher_ app. The _teacher_ app is used to create "game sets" that can then be
loaded into the _student_ app.

## Student

The student app is built in Unity 2022.3.62f1.

To build the student app, you will need the following dependencies installed:

 -  [Unity][] version 2022.3.62f1. You must use _exactly_ version 2022.3.62f1,
    otherwise Unity will complain at you when opening the project and things
    might break.

### Building

To create an executable build of the game:

 1. Open the `cs4474_student` directory as a Unity project.
 2. Click _File_ › _Build Settings_.
 3. Select your target platform. If the platform you want to build for does not
    appear, you may need to install additional build support for Unity.
 4. Click _Build_ in the bottom right corner and choose where to save the built
    executable.

You can then run the game by running the executable that was built in step 4.
Alternatively, you can select _Build and Run_ in step 4 to run the game
immediately after building.

### Running in the Unity editor

You can also run the game directly in the Unity editor. To do this:

 1. Open the `cs4474_student` directory as a Unity project.
 2. In the _Assets_ panel (usually at the bottom of the screen), open the
    _Scenes_ folder, then double-click the _Menu_ scene to open it.
 3. Click the ▶ _Play_ button at the top of the screen.

## Teacher

The teacher app is a single-page web application (SPA) written in React +
TypeScript and built with Vite and pnpm.

To build the teacher app, you will need the following dependencies installed:

 -  A reasonably recent version of [Node.js][]. Version 24.14.1 is recommended.
 -  A reasonably recent version of [pnpm][]. Version 10.33.0 is recommended.

The app can be run in one of two ways:

 -  Build for production and serve the resulting HTML, CSS, and JavaScript
 -  Run a live development server

### Building

To create a production build of the teacher app, open a terminal in the
`cs4474b_teacher` directory and run:

```sh
pnpm install
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
pnpm install
pnpm run dev
```

The app should now be accessible at http://localhost:5173/. Any changes you make
to the app's source code will be automatically reloaded.


[Node.js]: https://nodejs.org/en
[pnpm]: https://pnpm.io/installation
[Unity]: https://unity.com/

<!-- vim:sw=4 et: -->
