# def count_unique_dual_edges(n, edges):
#     # Use a set to store unique dual edges
#     dual_edges = set()

#     # Add each edge as a tuple to the set to ensure uniqueness
#     for edge in edges:
#         # Sort the endpoints to standardize the order
#         a, b = sorted(edge)
#         dual_edges.add((a, b))

#     # The number of unique dual edges
#     return len(dual_edges)

# Input: number of edges and then list of edges
n = int(input())
edges = []
for _ in range(n):
    edge = input().strip()
    edges.append((edge[0], edge[1]))

# Calculate the number of unique dual edges
# result = count_unique_dual_edges(n, edges)
print(n)


# بله، می‌توان گفت که تعداد یال‌های گراف دوگان همواره با تعداد یال‌های گراف اصلی \( G \) برابر است. این ویژگی از تعریف گراف دوگان ناشی می‌شود که در آن هر یال در گراف اصلی به یک یال در گراف دوگان متناظر است.

# گراف دوگان (Dual Graph) به این صورت ساخته می‌شود که هر یک از وجوه (faces) گراف اصلی \( G \) به یک راس در گراف دوگان تبدیل می‌شود و هر یال مرزی بین دو وجه در گراف اصلی به یک یال بین دو راس متناظر در گراف دوگان تبدیل می‌شود. بنابراین، تعداد یال‌ها در هر دو گراف یکسان خواهد بود.
