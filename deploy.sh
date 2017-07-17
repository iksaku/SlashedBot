#!/bin/bash
tmux send-keys -t SlashedBot C-c C-m 'cd ~/SlashedBot/ && git pull' C-m 'cd Engine/ && dotnet run' C-m