"use client";
import React from "react";

interface TaskDetailsProps {
    handleClick: () => void;
    read: boolean;
}

export default function TaskDetails({ handleClick, read }: TaskDetailsProps) {
    return (
        <div>
            <p>Task Details placeholder</p>
            <button onClick={handleClick}>Close</button>
        </div>
    );
}