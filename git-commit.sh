#!/bin/bash
################################################################################################################
# Stage all tracked files
################################################################################################################
git status
git add -A .
################################################################################################################
# Commit all new/modified files
################################################################################################################
git commit
################################################################################################################
# Commit to remote repo
################################################################################################################
git push -u origin --all
################################################################################################################
# End of script
################################################################################################################
read -p 'Done! Press enter'