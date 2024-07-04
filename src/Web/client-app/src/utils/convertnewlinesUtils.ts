interface ConvertNewlinesToBreaksProps {
  text: string;
}

const convertnewlinesUtils = (text: string): string => {
  return text.split('\n').join('<br />');
};

export default convertnewlinesUtils;
