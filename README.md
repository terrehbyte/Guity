# Guity

Guity is a proof of concept of an integrated Git client within Unity. It should
be capable of performing basic tasks:

- `git status` -- displaying the status of the working directory
- `git add` -- staging files for commit
- `git checkout <BRANCH>` -- switching branches

Since Unity editor plugins can not be installed independent of a project, Guity
does not support cloning and is dependant on external tools (Git, GitHub, etc.)
to clone a project containing Guity instead.

# Contributing

While this work is not currently accepting external work, others are encouraged
to fork and use this code however they see fit.

# License

Guity is licensed under the MIT License. Copyright (c) Terry Nguyen

See [LICENSE.md][LIC] for details or [THIRDPARTY.md][TP] for third party attributions.

[LIC]:LICENSE.md
[TP]:THIRDPARTY.md