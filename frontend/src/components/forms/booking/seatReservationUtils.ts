export const generateRandomColor = (passengerIdx: number) => {
  const colors = [
    "blue",
    "green",
    "yellow",
    "cyan",
    "pink",
    "indigo",
    "teal",
    "orange",
    "bluegray",
    "purple",
    "gray",
  ];

  const color = colors[passengerIdx];

  return `bg-${color}-600`;
};
