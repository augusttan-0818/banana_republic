"use client";
import React from "react";

interface ActivityDetailsProps {
    handleClick: () => void;
    read: boolean;
}

export default function ActivityDetails({ handleClick, read }: ActivityDetailsProps) {
    return (
        <div>
            <p>Activity Details placeholder</p>
            <button onClick={handleClick}>Close</button>
        </div>
    );
}