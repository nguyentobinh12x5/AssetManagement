import { useRef, useState, useEffect } from "react";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import "./TextToolTip.scss";

const isTextOverflowing = (element: HTMLElement | null) => {
  if (!element) return false;
  return element.scrollWidth > element.clientWidth;
};

const TextWithTooltip: React.FC<{ text: string }> = ({ text }) => {
  const textRef = useRef<HTMLDivElement | null>(null);
  const [isOverflowing, setIsOverflowing] = useState(false);

  useEffect(() => {
    if (textRef.current) {
      setIsOverflowing(isTextOverflowing(textRef.current));
    }
  }, [text]);

  return (
    <div ref={textRef} className="text-truncate">
      {isOverflowing ? (
        <OverlayTrigger placement="top" overlay={<Tooltip>{text}</Tooltip>}>
          <span>{text}</span>
        </OverlayTrigger>
      ) : (
        <span>{text}</span>
      )}
    </div>
  );
};

export default TextWithTooltip;
