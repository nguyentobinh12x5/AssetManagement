import React, { Suspense } from "react";
import InLineLoader from "../components/InlineLoader";

interface SuspenseProps {
    children: React.ReactNode
}
const SuspenseLoading: React.FC<SuspenseProps> = ({ children }) => {
    return (
        <Suspense fallback={<InLineLoader />}>{children}</Suspense>
    )
}

export default SuspenseLoading;